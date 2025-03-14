using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Photon.Pun.Demo.Cockpit;

public class login : MonoBehaviour
{
    public InputField userText;
    public InputField passwordText;
    public Canvas loginCanvas;
    public Canvas registrarCanvas;
    public Text erroMensaje;
    public Text userInfoText;
    public Text userId;
    private PlayerScript jugador;

    // Start is called before the first frame update
    private string apiUrl = "http://127.0.0.1:3000/api/jugadores/login";

    public void OnLoginButtonPressed()
    {
        StartCoroutine(LoginRequest());
    }
    public void RegistrarUsuario()
    {
        registrarCanvas.gameObject.SetActive(true);
        loginCanvas.gameObject.SetActive(false);
    }
    IEnumerator LoginRequest()
    {
        // Crear JSON con credenciales
        string jsonData = "{\"username\":\"" + userText.text + "\",\"password\":\"" + passwordText.text + "\"}";
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

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
                //Debug.Log("Login exitoso");
                loginCanvas.gameObject.SetActive(false);
                userText.text = "";
                passwordText.text = "";
                jugador.login = true;
                userInfoText.text = $"{response.username}";
                userId.text = $"{response.id}";

            }
            else
            {
                erroMensaje.text = "Error credenciales incorrectas: " + request.error;
                userText.text = "";
                passwordText.text = "";
            }
        }
    }
    [System.Serializable]
    public class TokenResponse
    {
        public string token;
        public int id;
        public string username;
    }
    void Start()
    {
        registrarCanvas.gameObject.SetActive(false);
        jugador = FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
