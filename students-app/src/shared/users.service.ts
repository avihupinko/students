import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environment";
import { SearchResult, Student } from "./models/student";
import { Observable } from "rxjs";



@Injectable({
    providedIn: 'root',
})
export class StudentsService {
    private serverUrl: string;
	
    constructor(private httpClient: HttpClient) {
        this.serverUrl = environment.serverUrl;
    }

    public get(firstName: string, pageSize: number, page: number, sort: string): Observable<SearchResult<Student>> {
        return this.httpClient.get<SearchResult<Student>>(`${this.serverUrl}/Students/Get`, { params: { firstName, pageSize, page, sort } });
    }

    
}