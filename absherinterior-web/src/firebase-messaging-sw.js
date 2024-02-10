// Scripts for firebase and firebase messaging
importScripts('https://www.gstatic.com/firebasejs/8.6.7/firebase-app.js');
importScripts('https://www.gstatic.com/firebasejs/8.6.7/firebase-messaging.js');

// Initialize the Firebase app in the service worker by passing in the
// messagingSenderId.


// console.log("test");
firebase.initializeApp({
  apiKey: "AIzaSyA87XxQUkWV_nxnieaKLZl-zP6yPfXKEzM",
    authDomain: "absher-abe51.firebaseapp.com",
    projectId: "absher-abe51",
    storageBucket: "absher-abe51.appspot.com",
    messagingSenderId: "446582853764",
    appId: "1:446582853764:web:dbecae22bb12d08e7bb2d1",
    measurementId: "G-Q8CHPDLQQM",
});


// Retrieve an instance of Firebase Messaging so that it can handle background
// messages.
const messaging = firebase.messaging();

if ("serviceWorker" in navigator) {
  window.addEventListener("load", function () {
    navigator.serviceWorker.register("/sw.js").then(
      function (registration) {
        // Registration was successful
        console.log(
          "ServiceWorker registration successful with scope: ",
          registration.scope
        );
      },
      function (err) {
        // registration failed :(
        console.log("ServiceWorker registration failed: ", err);
      }
    );
  });
}


// messaging.onMessage((payload) => {
//   console.log("test from payload");

//   console.log('Message received. ', payload);
//   // ...
// });
