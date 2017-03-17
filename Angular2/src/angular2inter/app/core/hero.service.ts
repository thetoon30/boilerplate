import { Injectable } from '@angular/core';
import { Hero } from '../model/hero';
import { HEROES } from '../mock-heroes';

@Injectable()
export class HeroService {
  getHeroes() {
      return Promise.resolve(HEROES);
  }
  getHero(id: number) {
  return this.getHeroes()
             .then(heroes => heroes.find(hero => hero.id === id));
  }
  // See the "Take it slow" appendix
  getHeroesSlowly() {
    return new Promise<Hero[]>(resolve =>
      setTimeout(() => resolve(HEROES), 2000) // 2 seconds
    );
  }
}