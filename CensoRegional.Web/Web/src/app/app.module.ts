import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SignalRService } from 'src/service/signalr.service';
import { HttpClientModule } from '@angular/common/http';
import { PersonService } from 'src/service/person.service';
import { ChartsModule } from 'ng2-charts';

@NgModule({
  declarations: [
    AppComponent,
    
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ChartsModule
  ],
  providers: [SignalRService, PersonService],
  bootstrap: [AppComponent]
})
export class AppModule { }
