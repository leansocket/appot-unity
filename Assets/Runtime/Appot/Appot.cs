using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

namespace UnityAppot
{
    public class Appot : MonoBehaviour
    {

        public enum Store
        {
            DEFAULT,
            HUAWEI,
            MI,
            SAMSUNG,
            OPPO,
            VIVO,
            TENCENT,
            APP_STORE
        }

        private const string endpoint = "http://www.plaifox.com:58080/appot";

        public string appid { get; }
        public string store { get; }

        public Appot(string appid) 
        {
            this.appid = appid;
            if (Application.platform == RuntimePlatform.Android) 
            {
                this.store = Store.DEFAULT.ToString();
            } 
            else if (Application.platform == RuntimePlatform.IPhonePlayer) 
            {
                this.store = Store.APP_STORE.ToString();
            } 
            else 
            {
                this.store = Store.DEFAULT.ToString();
            }
        }

        public Appot(string appid, Store storeEnum) 
        {
            this.appid = appid;
            this.store = storeEnum.ToString();
        }

        public delegate void AppInfoListResponseDelegate(string error, string res);

        public IEnumerator getAppInfoList(AppInfoListResponseDelegate callback) 
        {
            var request = new UnityWebRequest (endpoint + "/get_promotion_group_by_appid", "POST");
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.AppendFormat("\"appid\": \"{0}\",", appid);
            sb.AppendFormat("\"storeName\": \"{0}\"", store);
            sb.Append("}");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(sb.ToString());
            request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.error != null) 
            {
                callback(request.error, null);
                yield return null;
            } 

            callback(null, request.downloadHandler.text);
        }

    }
}
