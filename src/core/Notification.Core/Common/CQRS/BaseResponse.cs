using System;
using System.Net;

namespace Notification.Core.Common.CQRS
{
    public class BaseResponse<T> where T : View
    {
        public BaseResponse(T data)
        {
            StatusCode = (int)HttpStatusCode.OK;
            Mensagem = "Operação realizada com sucesso!";
            Dados = data;
        }

        public BaseResponse(HttpStatusCode code, string message)
        {
            StatusCode = (int)code;
            Mensagem = message;
        }

        public BaseResponse(HttpStatusCode code, List<ResponseErroView> errors)
        {
            Mensagem = "Ocorreu um erro.";
            StatusCode = (int)code;
            Erros = errors;
        }

        public BaseResponse(HttpStatusCode statusCode, string message, T data, List<ResponseErroView>? errors = null)
        {
            StatusCode = (int)statusCode;
            Mensagem = message;
            Dados = data;
            Erros = errors;
        }

        public int StatusCode { get; set; }
        public string Mensagem { get; set; } = null!;
        public T? Dados { get; set; }
        public List<ResponseErroView>? Erros { get; set; }
    }

    public class BaseListResponse<T> where T : View
    {
        public BaseListResponse(List<T?> data)
        {
            StatusCode = (int)HttpStatusCode.OK;
            Mensagem = "Operação realizada com sucesso!";
            Dados = data;
        }

        public BaseListResponse(HttpStatusCode code, string message)
        {
            StatusCode = (int)code;
            Mensagem = message;
        }

        public BaseListResponse(HttpStatusCode code, List<ResponseErroView> errors)
        {
            Mensagem = "Ocorreu um erro.";
            StatusCode = (int)code;
            Erros = errors;
        }

        public BaseListResponse(HttpStatusCode statusCode, string message, List<T?> data, List<ResponseErroView>? errors = null)
        {
            StatusCode = (int)statusCode;
            Mensagem = message;
            Dados = data;
            Erros = errors;
        }

        public int StatusCode { get; set; }
        public string Mensagem { get; set; } = null!;
        public List<T?> Dados { get; set; } = new List<T?>();
        public List<ResponseErroView>? Erros { get; set; }
    }

    public class ResponseErroView : View
    {
        public ResponseErroView(string codigo, string grupo, string mensagem)
        {
            Codigo = codigo;
            Grupo = grupo;
            Mensagem = mensagem;
        }

        protected ResponseErroView()
        {

        }


        public string Codigo { get; private set; }
        public string Grupo { get; private set; }
        public string Mensagem { get; private set; }
    }
}

