// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
  firebase: {
    apiKey: 'AIzaSyAKL84VNKWSiihosvSTD1z6vc44lAeAhWI',
    authDomain: 'musicportal-cecd8.firebaseapp.com',
    databaseURL: 'https://musicportal-cecd8.firebaseio.com',
    projectId: 'musicportal-cecd8',
    storageBucket: 'musicportal-cecd8.appspot.com',
    messagingSenderId: '872859256924'
  }
};
