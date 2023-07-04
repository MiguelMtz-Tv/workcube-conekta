namespace workcube_pagos.ViewModel.Req.Pago
{
    public class ConfirmationEmailReq
    {
        public DateTime Fecha       { get; set; }
        public string RazonSocial   { get; set; }
        public string IdAspNetUser  { get; set; }
        public string ServicioName  { get; set; }
        public string Folio         { get; set; }
        public decimal Monto        { get; set; }
        public decimal Descuento    { get; set; }
        public decimal Total        { get; set; }
        public string Last4         { get; set; }
        public string CardFunding   { get; set; }
        public string CardBrand     { get; set; }
        public string Email         { get; set; }
        public string Direccion     { get; set; }
    }
}
