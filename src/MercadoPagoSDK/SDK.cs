﻿using MercadoPago;
using MercadoPagoSDK.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace MercadoPago
{
    public class SDK
    {
        private const string DEFAULT_BASE_URL = "https://api.mercadopago.com";
                
        private static string UserToken = null;
        public static string RefreshToken = null;

        /// <summary>  
        ///  Property that represent the client secret token.
        /// </summary>
        public static string ClientSecret
        {
            get
            {
                return _clientSecret;
            }
            set
            {
                if (!string.IsNullOrEmpty(_clientSecret))
                {
                    throw new MPConfException("clientSecret setting can not be changed");
                }
                _clientSecret = value;
            }
        }
        static string _clientSecret = null;

        /// <summary>
        /// Property that represents a client id.
        /// </summary>
        public static string ClientId
        {
            get
            {
                return _clientId;
            }
            set
            {
                if (!string.IsNullOrEmpty(_clientId))
                {
                    throw new MPConfException("clientId setting can not be changed");
                }
                _clientId = value;
            }
        }
        static string _clientId = null;

        /// <summary>
        /// MercadoPago AccessToken.
        /// </summary>
        public static string AccessToken
        {
            get
            {
                return _accessToken;
            }
            set
            {
                if (!string.IsNullOrEmpty(_accessToken))
                {
                    throw new MPConfException("accessToken setting can not be changed");
                }
                _accessToken = value;
            }
        }
        static string _accessToken = null;

        /// <summary>
        /// MercadoPAgo app id.
        /// </summary>
        public static string AppId
        {
            get
            {
                return _appId;
            }
            set
            {
                if (!string.IsNullOrEmpty(_appId))
                {
                    throw new MPConfException("appId setting can not be changed");
                }
                _appId = value;
            }
        }
        static String _appId = null;

        /// <summary>
        /// Api base URL. Currently https://api.mercadopago.com
        /// </summary>
        public static string BaseUrl
        {
            get
            {
                return _baseUrl;
            }
        }
        static string _baseUrl = DEFAULT_BASE_URL;

        /// <summary>
        /// Dictionary based configuration. Valid configuration keys:
        /// clientSecret, clientId, accessToken, appId
        /// </summary>
        /// <param name="configurationParams"></param>
        public static void SetConfiguration(IDictionary<string, string> configurationParams)
        {
            if (configurationParams == null) throw new ArgumentException("Invalid configurationParams parameter");

            configurationParams.TryGetValue("clientSecret", out _clientSecret);
            configurationParams.TryGetValue("clientId", out _clientId);
            configurationParams.TryGetValue("accessToken", out _accessToken);
            configurationParams.TryGetValue("appId", out _appId);
        }

        /// <summary>
        /// Initializes the configurations based in a confiiguration object.
        /// </summary>
        /// <param name="config"></param>
        public static void SetConfiguration(MercadoPagoConfiguration config)
        {
			if (config == null) throw new ArgumentException("config parameter cannot be null");

            _clientSecret = GetConfigValue(config, "ClientSecret");
            _clientId = GetConfigValue(config, "ClientId");
            _accessToken = GetConfigValue(config, "AccessToken");
            _appId = GetConfigValue(config, "AppId");
        }

        /// <summary>
        /// Clean all the configuration variables
        /// (FOR TESTING PURPOSES ONLY)
        /// </summary>
        public static void CleanConfiguration()
        {
            _clientSecret = null;
            _clientId = null;
            _accessToken = null;
            _appId = null;
            _baseUrl = DEFAULT_BASE_URL;
        }

        /// <summary>
        /// Changes base Url
        /// (FOR TESTING PURPOSES ONLY)
        /// </summary>
        public static void SetBaseUrl(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        private static string GetConfigValue(MercadoPagoConfiguration config, string key)
        {
            string keyValue = config.Settings[key];
            return keyValue;
        }

        /// <summary>
        /// Get the access token pointing to OAuth.
        /// </summary>
        /// <returns>A valid access token.</returns>
        public static string GetAccessToken() 
        {
            if (string.IsNullOrEmpty(AccessToken))
            {
                AccessToken = MPCredentials.GetAccessToken();
            }
            return AccessToken;
        }

        /// <summary>
        /// Sets the access token.
        /// </summary>
        /// <param name="accessToken">Value of the access token.</param>
        public static void SetAccessToken(string accessToken)
        {
            if (!string.IsNullOrEmpty(AccessToken))
            {
                throw new MPException("Access_Token setting cannot be changed.");   
            }

            AccessToken = accessToken;
        }

        /// <summary>
        /// Gets the custom user token.
        /// </summary>
        /// <returns>User token to return.</returns>
        public static string GetUserToken()
        {
            return UserToken;
        }        

        public static JToken Get(String uri)
        {
            MPRESTClient client = new MPRESTClient();
            return client.ExecuteGenericRequest(HttpMethod.GET, uri, PayloadType.JSON, null);
        }

        public static JToken Post(string uri, JObject payload)
        {
            MPRESTClient client = new MPRESTClient();
            return client.ExecuteGenericRequest(HttpMethod.POST, uri, PayloadType.JSON, payload);
        }

        public static JToken Put(string uri, JObject payload)
        {
            MPRESTClient client = new MPRESTClient();
            return client.ExecuteGenericRequest(HttpMethod.PUT, uri, PayloadType.JSON, payload);
        }

    }
}
