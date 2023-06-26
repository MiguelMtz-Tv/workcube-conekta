import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask'
import { HotToastModule } from '@ngneat/hot-toast'
import { BrowserModule } from '@angular/platform-browser'
import { RouterModule } from '@angular/router'
import { HttpClientModule } from '@angular/common/http'
import { PdfViewerModule } from 'ng2-pdf-viewer'
import { NgxExtendedPdfViewerModule } from 'ngx-extended-pdf-viewer'


@NgModule({
    imports:[
        ReactiveFormsModule,
        FormsModule,
        NgxMaskDirective,
        NgxMaskPipe,
        HotToastModule.forRoot(),
        ReactiveFormsModule,
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        RouterModule,
        HttpClientModule,
        PdfViewerModule,
        NgxExtendedPdfViewerModule,
    ],

    exports:[
        ReactiveFormsModule,
        FormsModule,
        NgxMaskDirective,
        NgxMaskPipe,
        HotToastModule,
        ReactiveFormsModule,
        BrowserModule,
        RouterModule,
        HttpClientModule,
        PdfViewerModule,
        NgxExtendedPdfViewerModule
    ],
    providers: [provideNgxMask()],
})

export class LibrariesModule { }