export class Sessions {
    public static start(jsonSession : any) : boolean {
        if( jsonSession != undefined && jsonSession != null
            && jsonSession.id != undefined && jsonSession.id != null
            && jsonSession.token != undefined && jsonSession.token != null
            && jsonSession.nombreCompleto != undefined && jsonSession.nombreCompleto != null
            && jsonSession.idCliente != undefined && jsonSession.idCliente != null ) {
            
            this.setItem("id", jsonSession.id);
            this.setItem("token", jsonSession.token);
            this.setItem("nombreCompleto", jsonSession.nombreCompleto);
            this.setItem("idCliente", jsonSession.idCliente);

            return true;

        } else {
            //ELIMINAR SESION
            return false;
        }
    }

    public static setItem(key : string, value : any) {
        try {
            let jsonValue : string = JSON.stringify(value);
            localStorage.setItem(key, jsonValue);
        } catch (e) {
            localStorage.setItem(key, value);
        }
    }

    public static header() {
        let obj ={
            headers:{
                'Content-Type'      : 'application/json',
                'Authorization'     : 'Bearer ' + localStorage.getItem('token')
            }
        }
        return obj
    }
}
