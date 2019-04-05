using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace TcpTest
{
    public class TcpTestClient : MonoBehaviour
    {
        private TcpClient socketConnection;
        private Thread clientReceiveThread;

        public Text LogText;

        private void Start()
        {
            Connection();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SendMessage();
            }
        }


        void Connection()
        {
            try
            {
#if UNITY_ANDROID
                ClientAndroid();
#else
                clientReceiveThread = new Thread(new ThreadStart(ListenForData ));
                clientReceiveThread.IsBackground = true;
                if(clientReceiveThread != null)
                {
                    LogText.text = "Thread is working!";
                }
                clientReceiveThread.Start();
#endif
            }
            catch (Exception e)
            {
                Debug.Log("On Client Connect Exception " + e);
                LogText.text = "On Client Connect Exception " + e;
            }
        }


        TcpClient mySocket = new TcpClient();
        public NetworkStream theStream;
        public bool connect = false;
        StreamWriter theWriter;
        StreamReader theReader;
        public void ClientAndroid()
        {
            try
            {
                if (!connect)
                {
                    mySocket = new TcpClient("localhost", 7899);
                    theStream = mySocket.GetStream();
                    theWriter = new StreamWriter(theStream);
                    theReader = new StreamReader(theStream);
                    connect = true;
                    LogText.text = "connected";
                }
                string clientMessage = "This is a message from one of clients";
                byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
                theStream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
            }
            catch (Exception e)
            {
                LogText.text = "not connected";
            }

        }


        void ListenForData()
        {
            try
            {
                socketConnection = new TcpClient("localhost", 7899);
                byte[] bytes = new byte[1024];
                while (true)
                {
                    using (NetworkStream stream = socketConnection.GetStream())
                    {
                        int length;

                        while((length = stream.Read(bytes, 0, bytes.Length))!=0)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            // convert byte array to string message;
                            string serverMessage = Encoding.ASCII.GetString(incommingData);
                            Debug.LogError("Server msg received as : " +serverMessage);
                        }
                    }
                }

            }
            catch(Exception e)
            {
                Debug.LogError("Socket exception : "+ e);
            }
        }


        public void SendMessage()
        {
            if (socketConnection == null)
                return;

            try
            {
                NetworkStream stream = socketConnection.GetStream();
                if(stream.CanWrite)
                {
                    string clientMessage = "This is a message from one of clients";
                    byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
                    stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                    Debug.Log("Client sent msg");
                    LogText.text = "Client sent msg";
                }
            }
            catch(Exception e)
            {
                LogText.text = "Socket exception : " + e;
            }
        }
    }
}