import { ExceptionModel } from "./status.model";
import { GalleryModel } from "./gallery.model";

export class GalleryResult{
  public galleryModels: Array<GalleryModel>;
  public exceptionModel: ExceptionModel;
}
