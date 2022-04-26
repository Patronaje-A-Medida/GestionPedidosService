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
            public static readonly int GET_ORDER_FAILED = 10021;
            public static readonly int NOT_FOUND_ORDER = 10022;
            public static readonly int GET_ORDERS_CLIENT_FAILED = 10023;
            public static readonly int NOT_FOUND_CLIENT_ORDERS = 10024;

            public static readonly int GET_GARMENTS_FAILED = 10030;
            public static readonly int ADD_GARMENT_FAILED = 10031;
            public static readonly int GET_GARMENT_FAILED = 10032;
            public static readonly int NOT_FOUND_GARMENT = 10033;
            public static readonly int UPDATE_GARMENT_FAILED = 10034;

            public static readonly int ADD_IMAGE_PATTERN_FILES = 10090;
            

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
            public static readonly string GET_ORDER_FAILED = "No se pudo obtener la información del detalle de la orden requerida";
            public static readonly string NOT_FOUND_ORDER = "El detalle de la orden solicitada no existe";
            public static readonly string GET_GARMENTS_FAILED = "No se pudo obtener la información del catálogo de prendas del atelier";
            public static readonly string ADD_GARMENT_FAILED = "No se pudo guardar la información de la prenda";
            public static readonly string ADD_IMAGE_PATTERN_FILES = "No se pudo guardar los archivos de imágenes/patrones de la prenda";
            public static readonly string GET_GARMENT_FAILED = "No se pudo obtener la información del detalle de la prenda requerida";
            public static readonly string NOT_FOUND_GARMENT = "La prenda solicitada no existe";
            public static readonly string GET_ORDERS_CLIENT_FAILED = "No se pudo obtener las de las órdenes del cliente";
            public static readonly string NOT_FOUND_CLIENT_ORDERS = "Las ordenes de dicho cliente no existen";
            public static readonly string UPDATE_GARMENT_FAILED = "No se pudo actualizar la información de la prenda";
        }
    }
}
