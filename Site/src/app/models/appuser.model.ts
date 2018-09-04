import { ModelObject } from "./model-object.model";
import { AppUserContent } from "./appuser-content.model";
import { AppUserSettings } from "./appuser-settings.model";

export class AppUser extends ModelObject {
    firstName: string;
    lastName: string;
    city?: string;
    administrativeArea?: string;
    country?: string;

    dob?: Date;
    gender?: string;
    religion?: string;
    politics?: string;
    education?: string;
    profession?: string;
    interests?: string;

    password: string;
    handle: string;
    email: string;

    content?: AppUserContent;
    settings?: AppUserSettings;
}