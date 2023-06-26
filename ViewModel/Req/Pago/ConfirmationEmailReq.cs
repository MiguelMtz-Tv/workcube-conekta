namespace workcube_pagos.ViewModel.Req.Pago
{
    public class ConfirmationEmailReq
    {
        public DateTime Fecha       { get; set; }
        public string RazonSocial   { get; set; }
        public string IdAspNetUser  { get; set; }
        public string ServicioName  { get; set; }
        public string Folio         { get; set; }
        public long Monto           { get; set; }
        public long Descuento       { get; set; }
        public long Total           { get; set; }
        public string Last4         { get; set; }
        public string CardFunding   { get; set; }
        public string CardBrand     { get; set; }
        public string Email         { get; set; }
        public string Direccion     { get; set; }
    }
}
