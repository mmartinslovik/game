import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CharacterListComponent } from './character-list/character-list.component';
import { TokenInterceptorService } from './service/token-service/token-interceptor.service';
import { CharacterDetailsComponent } from './character-details/character-details.component';
import { UserLoginComponent } from './user-login/user-login.component';
import { FightDetailsComponent } from './fight-details/fight-details.component';

@NgModule({
  declarations: [
    AppComponent,
    CharacterListComponent,
    CharacterDetailsComponent,
    UserLoginComponent,
    FightDetailsComponent,
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'characters', component: CharacterListComponent },
      { path: 'characters/:characterId', component: CharacterDetailsComponent },
      { path: 'login', component: UserLoginComponent },
      { path: 'score', component: FightDetailsComponent },
    ]),
    HttpClientModule,
    FormsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
