using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class registerProgres : MonoBehaviour
{
    public Text player_id;
    public Text score;
    public Text lives;
    public Text time;
    public Text level;
    // Start is called before the first frame update
    private string apiUrl = "http://127.0.0.1:3000/api/jugadores/progreso";

    public IEnumerator RegisterProgresoRequest()
    {
        // Crear JSON con credenciales
        string jsonData = "{\"player_id\":\"" + player_id.text + "\",\"score\":\"" + score.text + "\",\"lives\":\"" + lives.text + "\",\"time\":\"" + time.text + "\",\"level\":\"" + level.text + "\"}";
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
        //Debug.Log(jsonData);
        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Extraer el token del JSON de respuesta
                string responseText = request.downloadHandler.text;
                TokenResponse response = JsonUtility.FromJson<TokenResponse>(responseText);
                PlayerPrefs.SetString("jwt_token", response.token); // Guardar el token
                Debug.Log("Registro exitoso");
            }
            else
            {
                Debug.Log("Error en el registro");
            }
        }
    }
    [System.Serializable]
    public class TokenResponse
    {
        public string token;
    }
}
