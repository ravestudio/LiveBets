import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { Event } from './Entity/event';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  /** GET heroes from the server */
  getEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(this.baseUrl + 'api/event')
  }
}
