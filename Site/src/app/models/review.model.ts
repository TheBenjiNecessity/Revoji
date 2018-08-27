import { ModelObject } from "./model-object.model";
import { AppUser } from "./appuser.model";
import { Reviewable } from "./reviewable.model";

export class Review extends ModelObject 
{
    appUserID: number;
    reviewableID: number;
    title: string;
    created: Date;
    comment: string;
    emojis: string;

    appUser: AppUser;
    reviewable: Reviewable;
}