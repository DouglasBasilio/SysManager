import { Injectable} from '@angular/core';
import { environment } from '../../environments/environment';
import { ProductTypeView } from '../components/product-type/models/product-type-view';
import { ServiceBase} from '../service-base/service-base';

@Injectable()
export class ProductTypeService extends ServiceBase<ProductTypeView>{
    constructor(){
        super({
               endpoint: `${environment.url_api}productType`
              }
            )
    }
}