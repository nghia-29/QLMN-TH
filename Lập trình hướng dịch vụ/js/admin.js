// --- 1. CODE CHẠY NGAY KHI MỞ TRANG (NHẬN TOKEN TỪ URL) ---
(function() {
    // Tìm trên thanh địa chỉ xem có ?data=... không (Do trang Login gửi sang)
    const params = new URLSearchParams(window.location.search);
    const dataMaHoa = params.get('data');

    if (dataMaHoa) {
        try {
            // Giải mã và lưu vào kho của trình duyệt
            const dataGoc = decodeURIComponent(dataMaHoa);
            localStorage.setItem('thong_tin_user', dataGoc);

            // Xóa sạch URL cho đẹp (bảo mật token)
            window.history.replaceState({}, document.title, window.location.pathname);
        } catch (e) {
            console.error("Lỗi nhận token:", e);
        }
    }
    
    // Kiểm tra: Nếu không có thông tin user thì đuổi về trang Đăng nhập
    const user = localStorage.getItem('thong_tin_user');
    if (!user) {
        alert("Phiên đăng nhập hết hạn! Vui lòng đăng nhập lại.");
        
        // QUAN TRỌNG: Sửa đường dẫn này đúng với nơi bạn để file login
        // Nếu dùng Live Server thì thường là cổng 5500
        window.location.href = "../html/dangnhap.html"; 
    }
})();

// --- 2. CODE ANGULARJS XỬ LÝ DỮ LIỆU ---
var app = angular.module('AppAdmin', []);

app.controller('CtrAdmin', function($scope, $http, $window) {
    
    // Lấy thông tin user từ bộ nhớ ra để dùng
    var jsonUser = localStorage.getItem('thong_tin_user');
    $scope.thongTinUser = jsonUser ? JSON.parse(jsonUser) : {};

    // =============================================================
    // CẤU HÌNH API GATEWAY (QUAN TRỌNG NHẤT)
    // =============================================================
    // 1. Dùng HTTP (không dùng HTTPS)
    // 2. Cổng 5000 (Gateway)
    // 3. Đường dẫn /gateway/admin/ sẽ được Ocelot chuyển sang Admin Service (Cổng 6001)
    const BASE_API = "http://localhost:5000/gateway/admin/GiaoVien"; 

    $scope.danhSachGiaoVien = [];
    $scope.hienForm = false;
    $scope.trangThaiForm = 'THEM'; 
    $scope.gvModel = {};

    // --- HÀM 1: LẤY DANH SÁCH ---
    $scope.layDanhSach = function() {
        console.log("Đang tải dữ liệu từ:", BASE_API); // Log để kiểm tra

        $http.get(BASE_API).then(function(res) {
            // API thường trả về dữ liệu ở dạng res.data
            // Nếu API trả về { data: [...] } thì dùng res.data.data
            $scope.danhSachGiaoVien = Array.isArray(res.data) ? res.data : (res.data.data || []);
            console.log("Đã tải xong:", $scope.danhSachGiaoVien);
        }, function(err) {
            console.error("Lỗi tải API:", err);
            
            // Xử lý thông báo lỗi dễ hiểu
            if (err.status === 502) {
                alert("Lỗi 502: Gateway không kết nối được Admin Service (Cổng 6001). Bạn đã bật project Admin chưa?");
            } else if (err.status === -1) {
                alert("Không thể kết nối Server (Cổng 5000). Kiểm tra lại Gateway!");
            } else {
                alert("Lỗi tải dữ liệu: " + (err.data?.message || err.statusText));
            }
        });
    };

    // Gọi hàm lấy danh sách ngay khi chạy
    $scope.layDanhSach();

    // --- CÁC HÀM XỬ LÝ GIAO DIỆN FORM ---
    $scope.moFormThem = function() {
        $scope.trangThaiForm = 'THEM';
        $scope.gvModel = {}; 
        $scope.hienForm = true;
    };

    $scope.dongForm = function() {
        $scope.hienForm = false;
    };

    $scope.chuanBiSua = function(gv) {
        $scope.trangThaiForm = 'SUA';
        $scope.gvModel = angular.copy(gv);
        
        // Chuyển chuỗi ngày tháng (nếu có) sang kiểu Date để hiển thị đúng trên input
        if ($scope.gvModel.NgaySinh) {
            $scope.gvModel.NgaySinh = new Date($scope.gvModel.NgaySinh);
        }
        $scope.hienForm = true;
    };

    // --- HÀM 2: LƯU DỮ LIỆU (THÊM / SỬA) ---
    $scope.luuDuLieu = function() {
        // Tùy vào backend của bạn viết API tên là gì (create/update hay dùng method POST/PUT)
        // Giả sử dùng POST cho cả 2 và phân biệt bằng URL
        var url = BASE_API + ($scope.trangThaiForm == 'THEM' ? '/create' : '/update');
        
        // Nếu Backend dùng chuẩn RESTful (POST để thêm, PUT để sửa) thì dùng code này:
        // var method = $scope.trangThaiForm == 'THEM' ? 'POST' : 'PUT';
        // var url = BASE_API; 

        $http.post(url, $scope.gvModel).then(function(res) {
            alert("Thành công!");
            $scope.dongForm();
            $scope.layDanhSach(); // Tải lại danh sách mới
        }, function(err) {
            var msg = err.data;
            if (err.data && err.data.message) msg = err.data.message;
            alert("Lỗi lưu dữ liệu: " + msg);
        });
    };

    // --- HÀM 3: XÓA ---
    $scope.xoaGiaoVien = function(id) {
        if (!confirm("Bạn có chắc chắn muốn xóa giáo viên này?")) return;
        
        // Giả sử API xóa là /delete?id=1 hoặc DELETE /1
        // Code mẫu cho dạng /delete?id=...
        $http.post(BASE_API + "/delete?id=" + id).then(function(res) {
            alert("Đã xóa!");
            $scope.layDanhSach();
        }, function(err) {
            alert("Lỗi xóa: " + (err.data?.message || err.statusText));
        });
    };

    // --- HÀM 4: ĐĂNG XUẤT ---
    $scope.dangXuat = function() {
        localStorage.removeItem('thong_tin_user');
        window.location.href = "../html/dangnhap.html";
    };
});