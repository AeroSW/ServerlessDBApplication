import { Pet } from './pet';

export class Cat implements Pet {
  name: string = null;
  address: string = null;
  owner: string = null;
  breed: string = null;
  breeder: string = null;
  outdoor: boolean = false;
  weight: number = null;
}
