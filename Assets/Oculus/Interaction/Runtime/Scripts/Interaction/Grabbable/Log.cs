using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Oculus.Interaction
{
      public class Log : MonoBehaviour
    {
        [System.Serializable]
        public class LogData
        {
            public string Name;
            public float Time;
            public string Type;
            public string UserId;
        }
        
        private static Log _instance;
        public static Log Instance => _instance;

        // Đảm bảo Log là Singleton và tồn tại giữa các scene
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                return;
            }

            if (_instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
            {
                Destroy(this.gameObject);   
            }
        }

        // Phương thức static để gọi từ bất kỳ đâu
        public static void WriteLog(string name, float time, string type)
        {
            string url = "https://computerarchitecture-f871c-default-rtdb.asia-southeast1.firebasedatabase.app/ComputerArchitecture.json";

            var data = new LogData
            {
                UserId = UserManager.Instance.UserId, // Lấy ID của người dùng
                Name = name,
                Time = time,
                Type = type
            };

            // Chuyển dữ liệu thành chuỗi JSON
            string jsonData = JsonUtility.ToJson(data);

            // Gọi coroutine thông qua instance
            _instance.StartCoroutine(_instance.SendLog(url, jsonData));
        }

        // Coroutine để gửi log
        private IEnumerator SendLog(string url, string jsonData)
        {
            using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
            {
                // Đặt nội dung JSON vào yêu cầu
                byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
                www.uploadHandler = new UploadHandlerRaw(jsonToSend);
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");
                
                yield return www.SendWebRequest();
            }
        }
    }
   
}
   
