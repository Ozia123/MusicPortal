import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';

export abstract class BaseService {
  constructor() { }

  protected handleError(error: any) {
    const applicationError = error.headers.get('Application-Error');

    if (applicationError) {
      return Observable.throw(applicationError);
    }

    let modelStateErrors: string = '';
    const serverError = error.json();

    if (!serverError.type) {
      for (const key in serverError) {
        if (serverError[key]) {
          modelStateErrors += serverError[key] + '\n';
        }
      }
    }

    modelStateErrors = modelStateErrors === '' ? null : modelStateErrors;
    return Observable.throw(modelStateErrors || 'Server error');
  }
}
