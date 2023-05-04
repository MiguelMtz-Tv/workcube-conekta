import { NgModule} from '@angular/core';
import { AppComponent } from './app.component';

import { AppRoutingModule } from './app-routing.module';
import { CommonModule } from '@angular/common';
import { LayoutsModule } from './layouts/layout.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    LayoutsModule
  ],
  exports:[
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
