import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Dog } from 'src/app/core/models/forms/pets/dog';
import { DogBreedService } from '../../core/services/pets/dogs/dog-breed.service';
import { Breed } from '../../core/models/breed/breed';
import { PetService } from '../../core/services/pets/pet.service';
import { InsertSuccess } from '../../core/models/service/InsertSuccess';

@Component({
  selector: 'app-dog-form',
  templateUrl: './dog-form.component.html',
  styleUrls: ['./dog-form.component.scss']
})
export class DogFormComponent implements OnInit {

  dogForm: FormGroup;
  dog_breeds: Breed[];
  private initial_form: object;

  constructor(
    private _fb: FormBuilder,
    private _dbs: DogBreedService,
    private _ps: PetService
  ) { }
  
  ngOnInit(): void {
    this.dog_breeds = [];
    this.buildForm();
    this.getDogBreeds();
  }

  submitDog() {
    if (this.dogForm.valid) {
      this.sendDogRecord();
    }
  }
  clearForm() {
    this.dogForm.reset(this.initial_form, { onlySelf: true, emitEvent: false });
    this.dogForm.markAsPristine({ onlySelf: true });
    this.dogForm.markAsUntouched({ onlySelf: true });
  }

  /* Private methods */

  private buildForm() {
    let tempDog: Dog = new Dog();
    let keys: string[] = Object.keys(tempDog);
    let group = {};
    keys.forEach(key => {
      if (typeof tempDog[key] === 'boolean') {
        group[key] = [""];
      }
      else {
        group[key] = ["", [Validators.required]];
      }
    });
    this.dogForm = this._fb.group(group);
    this.initial_form = this.dogForm.getRawValue();
  }
  private getDogBreeds() {
    this._dbs.getAllDogBreeds().subscribe((breed_list: Breed[]) => {
      this.dog_breeds = breed_list;
      this.dog_breeds.sort((a, b) => {
        if (a.name < b.name) return -1;
        return 1;
      });
    });
  }
  private sendDogRecord() {
    this._ps.insertDog(this.dogForm.value).subscribe((response: InsertSuccess) => {
      if (response.success) {
        console.log("SUCCESS!!!!!", response.key);
      }
      else {
        console.log("FAILURE.....");
      }
    });
  }
}
