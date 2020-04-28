import { Pet } from './pet';

export class Dog implements Pet {
  name: string = null;
  address: string = null;
  owner: string = null;
  breed: string = null;
  breeder: string = null;
  outdoor: boolean = false;
  attack: boolean = false;
  service: boolean = false;
  height: number = null;
  weight: number = null;
}
