import { ModelObject } from "./model-object.model";
import { AppUser } from "./appuser.model";
import { Review } from "./review.model";

export class ReviewLike extends ModelObject 
{
    appUserID: number;
    reviewID: number;
    type: string;
    created: Date;
    
    appUser: AppUser;
    review: Review;
}