import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Breed } from '../../../models/breed/breed';
import { map, catchError } from 'rxjs/operators';
import { environment } from '../../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CatBreedService {

  private static readonly url: string = 'https://api.thecatapi.com/v1/breeds';

  constructor(private _http: HttpClient) { }

  getAllCatBreeds(): Observable<Breed[]> {
    return this.getCatBreeds();
  }

  private getCatBreeds(): Observable<Breed[]> {
    const headers = new HttpHeaders()
      .append('x-api-key', environment.cat_key);
    return this._http.get<Breed[]>(CatBreedService.url, { headers: headers }).pipe(
      map((resp: object[]) => {
        let breedList: Breed[] = [];
        resp.forEach(obj => {
          if (obj.hasOwnProperty("name") && typeof obj['name'] === 'string') {
            let key: string = obj["name"].toLowerCase().replace(/[ \t]/g, '-');
            breedList.push({ id: key, name: obj['name'] });
          }
        });
        return breedList;
      })
    );
  }
}
