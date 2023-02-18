import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Metal } from 'src/app/_models/metal';
import { AssetsService } from 'src/app/_services/assets.service';

@Component({
  selector: 'app-assets-card',
  templateUrl: './assets-card.component.html',
  styleUrls: ['./assets-card.component.css']
})
export class AssetsCardComponent implements OnInit {
  @Input() asset: Metal;

  constructor(private assetsService: AssetsService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  addLike(asset: Metal) {
    this.assetsService.addLike(asset.id).subscribe(() => {
      this.toastr.success('You have liked' + asset.title);
    })
  }

}
