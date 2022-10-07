export interface UserModel {
  ssoID: {
    type: string;
    required: true;
    unique: true;
  };
  token: { 
    type : String, 
    required : false
  },
  displayName: String;
  favoritesList: number[]; // Estate IDs
}
