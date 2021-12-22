import { StatusModel } from "./status.model";
import { GalleryModel } from '../models/gallery/gallery.model';

export class GalleryResult{
  public galleryModels: Array<GalleryModel>;
  public statusModel: StatusModel;
}
