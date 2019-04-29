import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TinymceService {

  constructor() { }

  getSettings() {
    return {
      language:'zh_CN',
      skin_url: '/assets/tinymce/skins/ui/oxide',
      content_css: 'assets/tinymce/skins/ui/oxide/content.min.css',
      branding: false,

      height: 500,
      menubar: false,
      // tslint:disable-next-line:max-line-length
      plugins: 'textcolor colorpicker advlist autolink link lists charmap code print preview fullscreen paste image imagetools',
      // tslint:disable-next-line:max-line-length
      toolbar: `forecolor backcolor | bold italic underline strikethrough subscript superscript charmap | formatselect fontselect fontsizeselect | bullist numlist | alignleft aligncenter alignright | outdent indent | link unlink openlink image | code preview fullscreen`,

      images_upload_url: `${environment.apiUrlBase}/postimages`,
      images_upload_credentials: false,
      automatic_uploads: true,
      imagetools_cors_hosts: ['localhost:6001'],
      imagetools_toolbar: 'rotateleft rotateright | flipv fliph | editimage imageoptions',
      paste_data_images: true,
      paste_postprocess: function (plugin, args) {
        console.log(plugin);
        console.log(args);
        console.log(args.node);
      }
    };
  }
}
