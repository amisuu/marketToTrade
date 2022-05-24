import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { Metal } from 'src/app/_models/metal';
import { AssetsService } from 'src/app/_services/assets.service';

@Component({
  selector: 'app-assets-detail',
  templateUrl: './assets-detail.component.html',
  styleUrls: ['./assets-detail.component.css']
})
export class AssetsDetailComponent implements OnInit {
  metal: Metal;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor(private assetsService: AssetsService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadAsset();

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ]
  }

  getImages(): NgxGalleryImage[] {
    const imageUrls = [];
    for (const photo of this.metal.photos) {
      imageUrls.push({
        small: photo?.url,
        medium: photo?.url,
        big: photo?.url
      })
    }
    return imageUrls;
  }

  loadAsset() {
    this.assetsService.getAsset(this.route.snapshot.paramMap.get('id')).subscribe(metal => {
      this.metal = metal;
      this.galleryImages = this.getImages();
    })
  }
}
