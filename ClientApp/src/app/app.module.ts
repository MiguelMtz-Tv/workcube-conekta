import { NgModule} from '@angular/core';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { CommonModule } from '@angular/common';
import { LayoutsModule } from './layouts/layout.module';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './auth/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
  ],

  imports: [
    CommonModule,
    AppRoutingModule,  
    LayoutsModule,
  ],

  exports:[
  ],

  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true,
  }], 
  
  bootstrap: [AppComponent]
})
export class AppModule { }
