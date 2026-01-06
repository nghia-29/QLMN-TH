var app = angular.module('UngDungQuanLy', []);

app.controller('GiaoVienController', function($scope, $http) {
    
    // --- CẤU HÌNH API (Cổng 6001) ---
    var currentUrl = "http://localhost:6001"; 
    var apiBase = currentUrl + "/api/GiaoVien";

    $scope.dsGiaoVien = [];
    $scope.gv = {};
    $scope.cheDoSua = false;

    // 1. LOAD DANH SÁCH
    $scope.layDanhSach = function() {
        $http.get(apiBase + "/get-all")
        .then(function(res) {
            $scope.dsGiaoVien = res.data;
        }, function(err) {
            console.error(err);
        });
    };
    $scope.layDanhSach();

    // 2. CHỌN DÒNG (Để sửa)
    $scope.chonDong = function(item) {
        // Tự động map dữ liệu dù backend trả về hoa hay thường
        $scope.gv = {
            MaGiaoVien: item.maGiaoVien || item.MaGiaoVien,
            HoTen:      item.hoTen || item.HoTen,
            Sdt:        item.sdt || item.Sdt,
            Email:      item.email || item.Email,
            ChuyenMon:  item.chuyenMon || item.ChuyenMon,
            BangCap:    item.bangCap || item.BangCap
        };

        // Xử lý ngày sinh
        var rawDate = item.ngaySinh || item.NgaySinh;
        if (rawDate) {
            $scope.gv.NgaySinh = new Date(rawDate);
        }
        
        $scope.cheDoSua = true;
    };

    // 3. THÊM MỚI
    $scope.themMoi = function() {
        $http.post(apiBase + "/create", $scope.gv)
        .then(function(res) {
            alert("Thêm thành công!");
            $scope.layDanhSach();
            $scope.lamMoi();
        }, function(err) { alert("Lỗi: " + err.data); });
    };

    // 4. CẬP NHẬT
    $scope.capNhat = function() {
        $http.post(apiBase + "/update", $scope.gv)
        .then(function(res) {
            alert("Cập nhật thành công!");
            $scope.layDanhSach();
            $scope.lamMoi();
        }, function(err) { alert("Lỗi: " + err.data); });
    };

    // 5. XÓA
    $scope.xoa = function() {
        if (!confirm("Xóa giáo viên này?")) return;
        var id = $scope.gv.MaGiaoVien;
        $http.post(apiBase + "/delete?id=" + id)
        .then(function(res) {
            alert("Đã xóa!");
            $scope.layDanhSach();
            $scope.lamMoi();
        }, function(err) { alert("Lỗi: " + err.data); });
    };

    // 6. LÀM MỚI
    $scope.lamMoi = function() {
        $scope.gv = {};
        $scope.cheDoSua = false;
    };
});