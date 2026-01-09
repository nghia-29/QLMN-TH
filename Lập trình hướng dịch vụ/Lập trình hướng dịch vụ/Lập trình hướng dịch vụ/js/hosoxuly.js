var app = angular.module('ungDungHoSo', []);

app.controller('boDieuKhienHoSo', function($scope, $http) {
    
    // CẤU HÌNH: Đảm bảo port này khớp với Swagger (đang là 6003)
    var BASE_API_URL = 'http://localhost:6003/api'; 

    $scope.listSucKhoe = [];
    $scope.listBangDiem = [];

    // --- HÀM 1: LẤY DANH SÁCH SỨC KHỎE ---
    $scope.layDuLieuSucKhoe = function() {
        // Hiển thị thông báo đang tải...
        console.log("Đang kết nối đến: " + BASE_API_URL + '/HosoSucKhoe/get-all');
        
        $http.get(BASE_API_URL + '/HosoSucKhoe/get-all')
            .then(function(response) {
                console.log("Thành công! Dữ liệu Sức khỏe:", response.data);
                $scope.listSucKhoe = response.data;
            }, function(error) {
                // Nếu lỗi, in ra chi tiết
                console.error("LỖI SỨC KHỎE:", error);
                alert("Lỗi kết nối API Sức khỏe!\nChi tiết: " + JSON.stringify(error.statusText || "Không thể kết nối Server"));
            });
    };

    // --- HÀM 2: LẤY DANH SÁCH BẢNG ĐIỂM ---
    $scope.layDuLieuBangDiem = function() {
        $http.get(BASE_API_URL + '/BangDiem/get-all')
            .then(function(response) {
                console.log("Thành công! Dữ liệu Bảng điểm:", response.data);
                $scope.listBangDiem = response.data;
            }, function(error) {
                console.error("LỖI BẢNG ĐIỂM:", error);
            });
    };

    // Chạy ngay khi mở web
    $scope.layDuLieuSucKhoe();
    $scope.layDuLieuBangDiem();
});