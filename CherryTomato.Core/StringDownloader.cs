using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CherryTomato.Core
{
    class RequestState
    {
        const int BufferSize = 1024;
        
        public StringBuilder RequestData;
        public byte[] BufferRead;
        public WebRequest Request;
        public Stream ResponseStream;
        // Create Decoder for appropriate enconding type.
        public Decoder StreamDecode = Encoding.UTF8.GetDecoder();
        public StringDownloader.StringReceivedDelegate StringReceived;

        public RequestState()
        {
            BufferRead = new byte[BufferSize];
            RequestData = new StringBuilder(String.Empty);
            Request = null;
            ResponseStream = null;
        }
    }

    
    public class StringDownloader
    {
        const int BUFFER_SIZE = 1024;

        private string url;
        private StringReceivedDelegate received;

        public delegate void StringReceivedDelegate(string returnValue);

        public StringDownloader(string url, StringDownloader.StringReceivedDelegate received)
        {
            this.url = url;
            this.received = received;
        }

        public void Run()
        {
            var wr = WebRequest.Create(url);

            var state = new RequestState();
            state.Request = wr;
            state.StringReceived = received;

            try
            {
                wr.BeginGetResponse(RespCallback, state);
            }
            catch (Exception)
            {
                received(null);
            }
        }

        private void RespCallback(IAsyncResult ar)
        {
            var state = (RequestState)ar.AsyncState;
            var wr = state.Request;

            var resp = wr.EndGetResponse(ar);
            state.ResponseStream = resp.GetResponseStream();

            var iarRead = state.ResponseStream.BeginRead(state.BufferRead, 0, BUFFER_SIZE, ReadCallback, state);
            
        }

        private void ReadCallback(IAsyncResult asyncResult)
        {
            // Get the RequestState object from AsyncResult.
          RequestState rs = (RequestState)asyncResult.AsyncState;

          // Retrieve the ResponseStream that was set in RespCallback. 
          Stream responseStream = rs.ResponseStream;

          // Read rs.BufferRead to verify that it contains data. 
          int read = responseStream.EndRead( asyncResult );
          if (read > 0)
          {
             // Prepare a Char array buffer for converting to Unicode.
              Char[] charBuffer = new Char[BUFFER_SIZE];
             
             // Convert byte stream to Char array and then to String.
             // len contains the number of characters converted to Unicode.
          int len = 
             rs.StreamDecode.GetChars(rs.BufferRead, 0, read, charBuffer, 0);

             String str = new String(charBuffer, 0, len);

             // Append the recently read data to the RequestData stringbuilder
             // object contained in RequestState.
             rs.RequestData.Append(
                Encoding.ASCII.GetString(rs.BufferRead, 0, read));         

             // Continue reading data until 
             // responseStream.EndRead returns –1.
             IAsyncResult ar = responseStream.BeginRead(
                rs.BufferRead, 0, BUFFER_SIZE, 
                new AsyncCallback(ReadCallback), rs);
          }
          else
          {
             if(rs.RequestData.Length>0)
             {
                //  Display data to the console.
                string strContent;                  
                strContent = rs.RequestData.ToString();
             }
             // Close down the response stream.
             responseStream.Close();         
             // Set the ManualResetEvent so the main thread can exit.

             rs.StringReceived(rs.RequestData.ToString());
              //allDone.Set();                           
          }
          return;


        }
    }
}
