import { AuthenticationProvider } from "src/framework/Models/AuthenticationProvider";

export interface UserTokenIdModel
{
  authenticationProvider: AuthenticationProvider;
  jwtToken: string | null;
  picture: string | null;
  gender: string | null;
  id: string | null;
  email: string | null;
  verifiedEmail: boolean;
  name: string | null;
  givenName: string | null;
  familyName: string | null;
}
