//import { Injectable } from '@angular/core';
//import { Headers, Http, Response } from '@angular/http';
//import { Observable } from 'rxjs/Observable';
//import 'rxjs/add/operator/toPromise';

//import { Person } from '../model/person';
//@Injectable()
//export class PersonService {
//    private heroesUrl = 'api/Person';  // URL to web api

//    constructor(private http: Http) { }

//    getPersons(): Promise<Person[]> {
//        return this.http.get(this.heroesUrl, { headers: this.getHeaders() })
//            .toPromise()
//            .then(this.extractData)
//            .catch(this.handleError);
//    }

//    private getHeaders() {
//        let headers = new Headers();
//        headers.append('Accept', 'application/json');
//        return headers;
//    }

//    private extractData(res: Response) {
//        let body = res.json();
//        console.log(body);
//        return body.data || {};
//    }

//    private handleError(error: any): Promise<any> {
//        console.error('An error occurred', error); // for demo purposes only
//        return Promise.reject(error.message || error);
//    }
//}

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Headers, Http, RequestOptions, Response } from '@angular/http';
import { Person } from '../model/person';

// Statics
import 'rxjs/add/observable/throw';

// Operators
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/toPromise';
@Injectable()
export class PersonService {
    private heroesUrl = 'api/Person';  // URL to web API

    constructor(private http: Http) { }

    getPersons(): Observable<Person[]> {
        return this.http.get(this.heroesUrl, { headers: this.getHeaders() })
            .map(response => response.json() as Person[])
            .catch(this.handleError);
    }

    private getHeaders() {
        let headers = new Headers();
        headers.append('Accept', 'application/json');
        return headers;
    }

    private extractData(res: Response) {
        let body = res.json();
        console.log(body);
        return body.data || {};
    }

    private handleError(error: Response | any) {
        console.log(error);
        // In a real world app, we might use a remote logging infrastructure
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Observable.throw(errMsg);
    }
}