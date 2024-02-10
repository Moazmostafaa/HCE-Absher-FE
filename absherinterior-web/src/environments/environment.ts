// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
  baseApiUrl: "https://aidevapi.smartwaveeg.com/api/",
  filesBaseUrl: "https://aidevapi.smartwaveeg.com/api/",
  // baseApiUrl: "https://localhost:44340/api/",
  // filesBaseUrl: "https://localhost:44340/api/",
  DEFAULT_PAGE_SIZE: 10,
  DEFAULT_PAGE_SIZE_OPTIONS: [10, 20, 50],
  firebaseConfig: {
    apiKey: "AIzaSyA87XxQUkWV_nxnieaKLZl-zP6yPfXKEzM",
    authDomain: "absher-abe51.firebaseapp.com",
    projectId: "absher-abe51",
    storageBucket: "absher-abe51.appspot.com",
    messagingSenderId: "446582853764",
    appId: "1:446582853764:web:dbecae22bb12d08e7bb2d1",
    measurementId: "G-Q8CHPDLQQM",
  },
};
