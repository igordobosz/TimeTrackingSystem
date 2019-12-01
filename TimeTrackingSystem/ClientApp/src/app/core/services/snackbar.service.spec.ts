import { TestBed } from '@angular/core/testing';

import { SharedModule } from '../../shared/shared.module';
import { SnackbarService } from './snackbar.service';

describe('SnackbarService', () => {
    beforeEach(() => TestBed.configureTestingModule({
        imports: [SharedModule],
    }));

    it('should be created', () => {
        const service: SnackbarService = TestBed.get(SnackbarService);
        expect(service).toBeTruthy();
    });
});
