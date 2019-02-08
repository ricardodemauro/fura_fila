﻿using MercadoPago;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Collections.Specialized;

namespace MercadoPago
{
    public class MPRESTClient
    {

        public string ProxyHostName = null;
        public int ProxyPort = -1;

        #region Core Methods
        /// <summary>
        /// Class constructor.
        /// </summary>
        public MPRESTClient()
        {
            new MPRESTClient(null, -1);
        }

        /// <summary>
        /// Set class variables.
        /// </summary>
        /// <param name="proxyHostName">Proxy host to use.</param>
        /// <param name="proxyPort">Proxy port to use in the proxy host.</param>
        public MPRESTClient(string proxyHostName, int proxyPort)
        {
            this.ProxyHostName = proxyHostName;
            this.ProxyPort = proxyPort;
        }

        public JToken ExecuteGenericRequest(
            HttpMethod httpMethod,
            string path,
            PayloadType payloadType,
            JObject payload) 
        {
 

            if (SDK.GetAccessToken() != null) { 
                path = SDK.BaseUrl + path + "?access_token=" + SDK.GetAccessToken(); 

            }

            MPRequest mpRequest = CreateRequest(httpMethod, path, payloadType, payload, null, 0, 0);

            if (new HttpMethod[] { HttpMethod.POST, HttpMethod.PUT }.Contains(httpMethod))
            {
                Stream requestStream = mpRequest.Request.GetRequestStream();
                requestStream.Write(mpRequest.RequestPayload, 0, mpRequest.RequestPayload.Length);
                requestStream.Close();
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)mpRequest.Request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);
                    String StringResponse = reader.ReadToEnd();
                    return JToken.Parse(StringResponse);
                }

            }
            catch (WebException ex)
            {
                HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                Stream dataStream = errorResponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);
                String StringResponse = reader.ReadToEnd();
                return JToken.Parse(StringResponse);
            }

        }

		/// <summary>
		/// Execute a request to an endpoint.
		/// </summary>
		/// <param name="httpMethod">Method to use in the request.</param>
		/// <param name="path">Endpoint we are pointing.</param>
		/// <param name="payloadType">Type of payload we are sending along with the request.</param>
		/// <param name="payload">The data we are sending.</param>
		/// <param name="colHeaders">Extra headers to send with the request.</param>
		/// <returns>Api response with the result of the call.</returns>
		public MPAPIResponse ExecuteRequest(HttpMethod httpMethod, string path, PayloadType payloadType,
            JObject payload, 
            WebHeaderCollection colHeaders,
            int requestTimeout,
            int retries)
        {

            System.Diagnostics.Trace.WriteLine("Payload " + httpMethod + " request to " + path + " : " + payload); 
 
            try
            {
                return ExecuteRequestCore(httpMethod, path, payloadType, payload, colHeaders, requestTimeout, retries);
            }
            catch (Exception ex)
            {
                throw new MPRESTException(ex.Message);
            }
        }

        /// <summary>
        /// Core module implementation. Execute a request to an endpoint.
        /// </summary>
        /// <returns>Api response with the result of the call.</returns>
        public MPAPIResponse ExecuteRequestCore(
            HttpMethod httpMethod, 
            string path,
            PayloadType payloadType, 
            JObject payload, 
            WebHeaderCollection colHeaders,
            int connectionTimeout,
            int retries)
        {
             
                MPRequest mpRequest = CreateRequest(httpMethod, path, payloadType, payload, colHeaders, connectionTimeout, retries);

                string result = string.Empty; 

                if (new  HttpMethod[] { HttpMethod.POST, HttpMethod.PUT }.Contains(httpMethod))
                {
                    Stream requestStream = mpRequest.Request.GetRequestStream();
                    requestStream.Write(mpRequest.RequestPayload, 0, mpRequest.RequestPayload.Length);
                    requestStream.Close();
                }

                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)mpRequest.Request.GetResponse())
                    {  
                        return new MPAPIResponse(httpMethod, mpRequest.Request, payload, response);
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError){
                        HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                        return new MPAPIResponse(httpMethod, mpRequest.Request, payload, errorResponse); 
                    } else {
                        if (--retries == 0)
                            throw;
                        return ExecuteRequestCore(httpMethod, path, payloadType, payload, colHeaders, connectionTimeout, retries);
                    }
                     
                }
             
        }

        /// <summary>
        /// Create a request to use in the call to a certain endpoint.
        /// </summary>
        /// <returns>Api response with the result of the call.</returns>
        public MPRequest CreateRequest(HttpMethod httpMethod,
            string path,
            PayloadType payloadType,
            JObject payload,
            WebHeaderCollection colHeaders,
            int connectionTimeout,
            int retries)
        {

            if (string.IsNullOrEmpty(path))
                throw new MPRESTException("Uri can not be an empty string.");

            if (httpMethod.Equals(HttpMethod.GET))
            {
                if (payload != null)
                {
                    throw new MPRESTException("Payload not supported for this method.");
                }
            }
            else if (httpMethod.Equals(HttpMethod.POST))
            {
                //if (payload == null)
                //{
                //    throw new MPRESTException("Must include payload for this method.");
                //}
            }
            else if (httpMethod.Equals(HttpMethod.PUT))
            {
                if (payload == null)
                {
                    throw new MPRESTException("Must include payload for this method.");
                }
            }
            else if (httpMethod.Equals(HttpMethod.DELETE))
            {
                if (payload != null)
                {
                    throw new MPRESTException("Payload not supported for this method.");
                }
            }

            MPRequest mpRequest = new MPRequest();
            mpRequest.Request = (HttpWebRequest)HttpWebRequest.Create(path);
            mpRequest.Request.Method = httpMethod.ToString();

            if(connectionTimeout > 0)
            {
                mpRequest.Request.Timeout = connectionTimeout;
            }            

            if (colHeaders != null)
            {
                foreach (var header in colHeaders)
                {
                    mpRequest.Request.Headers.Add(header.ToString(), colHeaders[header.ToString()]);
                }                
            }

            mpRequest.Request.ContentType = "application/json";
            mpRequest.Request.UserAgent = "MercadoPago DotNet SDK/1.0.30";
            mpRequest.Request.Headers.Add("x-product-id", "BC32BHVTRPP001U8NHL0");


            if (payload != null) // POST & PUT
            {
                byte[] data = null;
                if(payloadType != PayloadType.JSON)
                {
                    var parametersDict = payload.ToObject<Dictionary<string, string>>();
                    StringBuilder parametersString = new StringBuilder();
                    parametersString.Append(string.Format("{0}={1}", parametersDict.First().Key, parametersDict.First().Value));
                    parametersDict.Remove(parametersDict.First().Key);
                    foreach (var value in parametersDict)
                    {
                        parametersString.Append(string.Format("&{0}={1}", value.Key, value.Value.ToString()));
                    }

                    data = Encoding.ASCII.GetBytes(parametersString.ToString());
                }
                else
                {
                    data = Encoding.ASCII.GetBytes(payload.ToString());
                }
                                
                mpRequest.Request.ContentLength = data.Length;
                mpRequest.Request.ContentType = payloadType == PayloadType.JSON ? "application/json" : "application/x-www-form-urlencoded";
                mpRequest.RequestPayload = data;
            }

            return mpRequest;
        }
               
        #endregion
    }
}

