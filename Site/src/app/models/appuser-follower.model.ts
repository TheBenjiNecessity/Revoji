import { AppUser } from "./appuser.model";

export class AppUserFollowing {
    followerId: number;
    followingId: number;
    created: Date;

    follower: AppUser;
    following: AppUser;
}