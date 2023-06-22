// declare var swal : any;

import Swal2 from 'sweetalert2'
import Swal from 'sweetalert2'

// CommonJS
// const objSwal2 = require('sweetalert2');

export class Dialogs {

  private buttonColor : string          = "#3f83f8";
  private buttonCancelColor : string    = "#ebf5ff";
  private confirmText : string          = "Aceptar";
  private cancelText : string           = "Cancelar";

  constructor() { }

  public Confirm(title: string, text: string, confirmText: string, cancelText: string, fnConfirm : any = null, fnCancel : any = null, isHTML : boolean = false) {

    if(confirmText == undefined || confirmText == null) confirmText = this.confirmText;
    if(cancelText == undefined || cancelText == null) cancelText = this.cancelText;

    var texto =  isHTML ? "" : text;
    var html  = isHTML ? text : "";

    Swal2.fire({
      title                     : title,
      text                      : texto,
      html                      : html,
      type                      : 'question',
      width                     : 600,
      
      confirmButtonColor        : this.buttonColor,
      confirmButtonText         : confirmText,

      showCancelButton          : true,
      cancelButtonColor         : this.buttonCancelColor,
      cancelButtonText          : cancelText,
      // closeOnConfirm            : true,
      // closeOnCancel             : true
    }).then((handlerEvent) => {
      if(handlerEvent.value) {
        if( fnConfirm != undefined && fnConfirm != null ) fnConfirm();
      } else {
        if( fnCancel != undefined && fnCancel != null ) fnCancel();
      }
    });

  }


  public Timer(title: string, text: string, timer : number, 
    // confirmText: string = null, cancelText: string = null, 
    // fnConfirm = null, fnCancel = null, 
    fnFinish : any = null) {

    // if(confirmText == undefined || confirmText == null) confirmText = this.confirmText;
    // if(cancelText == undefined || cancelText == null) cancelText = this.cancelText;

    Swal2.fire({
      title                     : title,
      text                      : text,
      type                      : 'info',
      width                     : 600,
      timer                     : timer
      
      // confirmButtonColor        : this.buttonColor,
      // confirmButtonText         : confirmText,

      // showCancelButton          : true,
      // cancelButtonText          : cancelText,
      // closeOnConfirm            : true,
      // closeOnCancel             : true
    }).then((handlerEvent) => {
      // if(handlerEvent.value) {
      //   if( fnConfirm != undefined && fnConfirm != null ) fnConfirm();
      // } else {
      //   if( fnCancel != undefined && fnCancel != null ) fnCancel();
      // }
      if( fnFinish != undefined && fnFinish != null ) fnFinish();
    });



//     let timerInterval;
//     Swal2.fire({
//       title: 'Auto close alert!',
//       html: 'I will close in <strong></strong> seconds.',
//       timer: 2000,
//       onBeforeOpen: () => {
//         Swal2.showLoading()
//         timerInterval = setInterval(() => {
//         Swal2.getContent().querySelector('strong')
//         .textContent = Swal2.getTimerLeft()
//     }, 100)
//   },
//   onClose: () => {
//     clearInterval(timerInterval)
//   }
// }).then((result) => {
//   if (
//     // Read more about handling dismissals
//     result.dismiss === Swal2.DismissReason.timer
//   ) {
//     console.log('I was closed by the timer')
//   }
// })

  }


  public Alert(title: string, text: string, confirmText: string = "Aceptar", fnAceptar : any = null, isHTML : boolean = false){

    if(confirmText == undefined || confirmText == null) confirmText = this.confirmText;

    var texto =  isHTML ? "" : text;
    var html  = isHTML ? text : "";

    Swal2.fire({
      title                     : title,
      text                      : texto,
      html                      : html,
      type                      : 'error',
      width                     : 600,
      
      confirmButtonColor        : this.buttonColor,
      confirmButtonText         : confirmText,

      showCancelButton          : false,

      // closeOnConfirm            : true,
      // closeOnCancel             : true
    }).then((handlerEvent) => {
      if(handlerEvent.value && fnAceptar != undefined && fnAceptar != null) {
        fnAceptar();
      }
    });

  }


  public Success(title: string, text: string, confirmText: string = "Aceptar", fnConfirm : any = null) {

    if(confirmText == undefined || confirmText == null) confirmText = this.confirmText;

    Swal2.fire({
      title                     : title,
      text                      : text,
      type                      : 'success',
      width                     : 600,
      
      confirmButtonColor        : this.buttonColor,
      confirmButtonText         : confirmText,

      showCancelButton          : false,

      // closeOnConfirm            : true,
      // closeOnCancel             : true
    }).then((handlerEvent) => {
      if(fnConfirm != undefined && fnConfirm != null) {
        fnConfirm();
      }
    });

  }

  public Info(title: string, text: string, confirmText: string = "Aceptar", fnConfirm : any = null) {

    if(confirmText == undefined || confirmText == null) confirmText = this.confirmText;

    Swal2.fire({
      title                     : title,
      text                      : text,
      type                      : 'info',
      width                     : 600,
      
      confirmButtonColor        : this.buttonColor,
      confirmButtonText         : confirmText,

      showCancelButton          : false,

      // closeOnConfirm            : true,
      // closeOnCancel             : true
    }).then((handlerEvent) => {
      if(fnConfirm != undefined && fnConfirm != null) {
        fnConfirm();
      }
    });

  }

  public TimerWithoutButton(title: string, text: string, timer : number, fnFinish : any = null) {

    Swal2.fire({
      title                     : title,
      text                      : text,
      type                      : 'info',
      width                     : 600,
      timer                     : timer,
      showConfirmButton         : false,
      allowOutsideClick         : false

    }).then((handlerEvent) => {
      if( fnFinish != undefined && fnFinish != null ) fnFinish();
    });

  }

  public SuccessToast(title : string) {
    
    Toast.fire({
      type: "success",
      title: title
    });

  }

  public ErrorToast(title : string) {
    
    Toast.fire({
      type: "error",
      title: title
    });

  }

  public WarningToast(title : string) {
    
    Toast.fire({
      type: "warning",
      title: title
    });

  }

  public QuestionToast(title : string) {
    
    Toast.fire({
      type: "question",
      title: title
    });

  }

  public InfoToast(title : string) {
    
    Toast.fire({
      type: "info",
      title: title
    });

  }

  
}

const Toast = Swal.mixin({
  toast: true,
  position: 'top',
  showConfirmButton: false,
  timer: 3000
});
