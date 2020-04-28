import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Dog } from '../../models/forms/pets/dog';
import { InsertSuccess } from '../../models/service/InsertSuccess';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PetService {
  private static readonly endpoint: string = "Pet/Dog";

  constructor(private _http: HttpClient) { }

  insertDog(dog: Dog): Observable<InsertSuccess> {
    return this.postDog(dog);
  }
  private postDog(dog: Dog): Observable<InsertSuccess> {
    const url = `${environment.pet_url}/${PetService.endpoint}`
    return this._http.post(url, dog).pipe(
      catchError(e => this.handleError(e))
    )
  }
  private handleError(err: HttpErrorResponse):Observable<any> {
    return of();
  }
}
