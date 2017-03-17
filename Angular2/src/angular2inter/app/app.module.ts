import { NgModule }       from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { FormsModule }    from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent }   from './app.component';
import { routing }        from './app.routing';

import { HeroesComponent }      from './heroes.component';
import { DashboardComponent }   from './dashboard.component';
import { HeroDetailComponent }  from './hero-detail.component';
import { UsersComponent } from './users/users.component';

import { HeroService } from './core/hero.service';
import { PersonService } from './core/person.service';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule, 
    routing
  ],
  declarations: [
    AppComponent,
    HeroesComponent,
    DashboardComponent,
    HeroDetailComponent,
    UsersComponent
  ],
  providers: [
      HeroService,
      PersonService
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule {
}
