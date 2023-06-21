namespace workcube_pagos.ViewModel.Res.Usuario
{
    public class LoginRes
    {
        public string Token             { get; set; }
        public string Id                { get; set; }
        public int IdCliente            { get; set; }
        public string NombreCompleto    { get; set; }
        public string Email             { get; set; }
        public DateTime Expires         { get; set; }

    }
}
