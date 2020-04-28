import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { of, Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Breed } from '../../../models/breed/breed';

@Injectable({
  providedIn: 'root'
})
export class DogBreedService {

  private static readonly url: string = 'https://dog.ceo/api/breeds/list/all';

  constructor(private _http: HttpClient) { }

  getAllDogBreeds(): Observable<Breed[]> {
    return this.getDogBreeds();
  }

  private getDogBreeds(): Observable<Breed[]> {
    return this._http.get<Breed[]>(DogBreedService.url).pipe(
      map((response: object) => {
        let breedList: Breed[] = [];
        if (response.hasOwnProperty('status') && response['status'] === 'success') {
          if (response.hasOwnProperty('message') && typeof response['message'] === 'object') {
            let breeds = Object.keys(response['message']);
            breeds.forEach((breed: string) => {
              if (response['message'][breed] && response['message'][breed].length > 0) {
                response['message'][breed].forEach((sub_breed: string) => {
                  breedList.push({
                    id: `${breed}-${sub_breed}`,
                    name: `${sub_breed} ${breed}`
                  });
                });
              }
              else if (response['message'][breed]) {
                breedList.push({
                  id: breed,
                  name: breed
                });
              }
            })
          }
        }
        return breedList;
      })
    )
  }
}
