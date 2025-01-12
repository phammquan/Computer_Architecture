using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// namespace Computer_Architecture
// {
    public class Log : MonoBehaviour
    {
        [System.Serializable]
        public class LogData
        {
            public string Name;
            public string Message;
            public string UserId;
        }
        
        private static Log _instance;

        // Đảm bảo Log là Singleton và tồn tại giữa các scene
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Phương thức static để gọi từ bất kỳ đâu
        public static void WriteLog(string name, string message)
        {
            string url = "https://test-cfc1a-default-rtdb.firebaseio.com/products.json";

            var data = new LogData
            {
                UserId = UserManager.Instance.UserId, // Lấy ID của người dùng
                Name = name,
                Message = message
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
// }