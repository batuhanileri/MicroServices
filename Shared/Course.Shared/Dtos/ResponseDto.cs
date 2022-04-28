using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Course.Shared.Dtos
{
    public class ResponseDto<T>
    {
        public T Data { get; private set; }

        [JsonIgnore] //Response kısımda zaten bir status code verdiği için tekrardan vermesin ignore etsin
        public int StatusCode { get; private set; }

        [JsonIgnore] //Response kısımda zaten success olup olmadığını gösterdiği için tekrardan göstermesin ignore etsin
        public bool IsSuccessFull { get; private set; }
        public List<string> Errors { get; set; } //Hatalarımız burada gelicek


        //static metodlar class içerisinde tanımlanarak nesne oluşturmana yardımcı olur.
        public static ResponseDto<T> Success(T data, int statusCode) // data ve status codu birlikte gösteriyoruz.
        {
            return new ResponseDto<T>
            {
                Data = data,
                StatusCode = statusCode,
                IsSuccessFull = true
            };
        }

        public static ResponseDto<T> Success(int statusCode) // Başarılı olabilir ama data almayabilir sadece status codu gösteriyoruz.
        {
            return new ResponseDto<T>
            {
                Data = default(T),
                StatusCode = statusCode,
                IsSuccessFull = true
            };
        }

        public static ResponseDto<T> Fail(List<string> errors,int statusCode) 
        {
            return new ResponseDto<T>
            {
                Errors=errors,
                StatusCode = statusCode,
                IsSuccessFull = false
            };
        }

        public static ResponseDto<T> Fail(string error, int statusCode)
        {
            return new ResponseDto<T>
            {
                Errors = new List<string> 
                { 
                    error
                },
                StatusCode = statusCode,
                IsSuccessFull = false
            };
        }
    }
}
