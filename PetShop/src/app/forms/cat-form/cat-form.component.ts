import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Breed } from '../../core/models/breed/breed';
import { Cat } from '../../core/models/forms/pets/cat';
import { CatBreedService } from '../../core/services/pets/cats/cat-breed.service';

@Component({
  selector: 'app-cat-form',
  templateUrl: './cat-form.component.html',
  styleUrls: ['./cat-form.component.scss']
})
export class CatFormComponent implements OnInit {

  catForm: FormGroup;
  cat_breeds: Breed[];
  private initial_form: object;

  constructor(
    private _fb: FormBuilder,
    private _cbs: CatBreedService
  ) { }

  ngOnInit(): void {
    this.cat_breeds = [];
    this.buildForm();
    this.getCatBreeds();
  }
  submitCat() {}
  clearForm() {
    this.catForm.reset(this.initial_form, { onlySelf: true, emitEvent: false });
    this.catForm.markAsPristine({ onlySelf: true });
    this.catForm.markAsUntouched({ onlySelf: true });
  }


/* Private methods */

  private buildForm() {
    let tempCat: Cat = new Cat();
    let keys: string[] = Object.keys(tempCat);
    let group = {};
    keys.forEach(key => {
      group[key] = [tempCat[key], [Validators.required]];
    });
    this.catForm = this._fb.group(group);
    this.initial_form = this.catForm.getRawValue();
  }
  private getCatBreeds() {
    this._cbs.getAllCatBreeds().subscribe((breed_list: Breed[]) => {
      this.cat_breeds = breed_list;
    });
  }
}
