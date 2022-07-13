// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  ConnectedServices: {
    Authorization: "https://authorizationmicroservice20220713175548.azurewebsites.net/",
    Product: "https://productmicroservice20220713172150.azurewebsites.net/",
    ServiceBooking: "https://servicebookingmicroservice20220713184004.azurewebsites.net/",
    User: "https://usermicroservice20220713180703.azurewebsites.net/"
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.

// Product: https://productmicroservice20220713172150.azurewebsites.net
// Authorization: https://authorizationmicroservice20220713175548.azurewebsites.net
// User: https://usermicroservice20220713180703.azurewebsites.net
