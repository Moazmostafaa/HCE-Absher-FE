import { NbAuthJWTToken, NbPasswordAuthStrategy } from "@nebular/auth";
import { environment } from "../../../environments/environment";
import { defaultSettings } from "./auth-default-settings";

export const authConfig = {
  strategies: [
    NbPasswordAuthStrategy.setup({
      name: "email",
      baseEndpoint: environment.baseApiUrl,
      token: {
        class: NbAuthJWTToken,
        key: "entity.token", // this parameter tells where to look for the token
      },
      login: {
        endpoint: "account/Login",
        method: "post",
        redirect: {
          success: "/pages/dashboard", // welcome page path
          failure: null, // stay on the same page
        },
      },
      logout: {
        endpoint: "account/Logout",
        method: "post",
        redirect: {
          success: "/auth/login",
          failure: "/auth/login",
        },
      },
      resetPass: {
        endpoint: "account/ChangePassword",
        method: "post",
      },
    }),
  ],
  forms: defaultSettings.forms,
};
