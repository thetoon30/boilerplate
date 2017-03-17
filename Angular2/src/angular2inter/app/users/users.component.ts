import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Person } from '../model/person';
import { PersonService } from '../core/person.service';

@Component({
    selector: 'app-users',
    templateUrl: 'app/users/users.component.html',
    styleUrls: ['app/users/users.component.css']
})

export class UsersComponent implements OnInit {
    users: Person[] = [];
    errorMessage: string;
    constructor(private router: Router, private personService: PersonService) { }

    getPersons(): void {
        this.personService.getPersons().subscribe(
            users => this.users = users,
            error => this.errorMessage = <any>error);

        //this.personService.getPersons().then(users => this.users = users);
    }

    ngOnInit(): void {
        this.getPersons();
    }
}
