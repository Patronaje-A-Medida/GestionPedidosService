using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Utils
{
    public struct ErrorsUtil
    {
        public struct ErrorsCode
        {
            public static readonly int GENERIC_ERROR = 10000;

            public static readonly int INVALID_MODEL_ERROR = 10001;
            public static readonly int ADD_CONTEXT_ERROR = 10002;
            public static readonly int GET_CONTEXT_ERROR = 10003;

            public static readonly int REGISTER_USER_FAILED = 10010;

            public static readonly int GET_ORDERS_FAILED = 10020;
        }

        public struct ErrorMessages
        {
            public static readonly string GENERIC_ERROR_400 = "No se pudo obtener la información requerida";
            public static readonly string GENERIC_ERROR_500 = "Error Interno del Sistema";
            public static readonly string INVALID_MODEL_ERROR = "";
            public static readonly string ADD_CONTEXT_ERROR = "No se pudo guardar el registro";
            public static readonly string GET_CONTEXT_ERROR = "No se pudo obtener la información requerida";
            public static readonly string REGISTER_USER_FAILED = "No se pudo registrar el usuario";
            public static readonly string GET_ORDERS_FAILED = "No se pudo obtener la información de las órdenes de pedidos del atelier";
        }
    }
}
