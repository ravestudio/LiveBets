import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Event, Outcomes } from './Entity/event';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  /** GET heroes from the server */
  getEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(this.baseUrl + 'api/event')

    .pipe(
      map(resp => this.Convert(resp))
      );
  }

  Convert(obj): Event[] {

    var result: Event[] = new Array();

    obj.forEach(_event => {

      var event: Event = new Event();
      event.gameId = _event.gameId;
      event.matchTime = _event.matchTime;
      event.eventTitle = _event.eventTitle;

      var _score = _event.scores.filter(s => s.name == "total")[0];

      event.score = _score.team1 + ":" + _score.team2;

      var _outcomes = _event.outcomesWinner.filter(o => o.name == "main");

      if (_outcomes != null && _outcomes.length > 0) {
        event.outcomes = new Outcomes();

        event.outcomes.team1 = _outcomes[0].win1;
        event.outcomes.team2 = _outcomes[0].win2;
        event.outcomes.x = _outcomes[0].x;
      }

      result.push(event);
    });

    return result;

  }
}
